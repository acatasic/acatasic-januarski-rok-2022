import { Kategorija } from "./Kategorija.js";
import {Film} from "./film.js"
export class ProdKuca {
   
    constructor(id, naziv) {
        this.id = id;
        this.naziv = naziv;
        this.kontejner = null;
        this.kategorije = [];
    }

    crtajPK(host) {

        if (!host) {
            throw new Exception("Roditeljski element ne postoji");
        }
        this.kontejner = document.createElement("div");
        this.kontejner.classList.add("kontejner");
        host.appendChild(this.kontejner);
        this.crtajFormu(this.kontejner);
    }
    crtajFormu(host) {
        const kontForma = document.createElement("div");
        kontForma.className = "kontForma";
        host.appendChild(kontForma);

        var divZaIzborKategorije = document.createElement("div");

        const kontForma1 = document.createElement("div");
        kontForma1.className = "kontForma1";
        kontForma.appendChild(kontForma1);

        let labela = document.createElement("label");
        labela.innerHTML = "kategorija:"
        divZaIzborKategorije.appendChild(labela);
        kontForma1.appendChild(divZaIzborKategorije);

        var sel = document.createElement("select");
        sel.name = "selectZaIzborKategorije";
        let labela1 = document.createElement("label");

        divZaIzborKategorije.appendChild(labela1);
        divZaIzborKategorije.appendChild(sel);
        kontForma1.appendChild(divZaIzborKategorije);

        fetch("https://localhost:5001/zoo/PreuzmiKategorije/" + this.id, {
            method: "GET",
        }).then(p => {
            p.json().then(data => {
                data.forEach((d, index) => {
                    var opcija = document.createElement("option");
                    opcija.innerHTML = d.tip;
                    opcija.value = d.id;
                    sel.appendChild(opcija);
                })
            })
        })

        var izabranaKategorija;

        var divZaIzborFilmova = document.createElement("div");
        divZaIzborFilmova.className="divZaIzborFilmova";
        labela = document.createElement("label");
        labela.innerHTML = "filmovi:";
        kontForma1.appendChild(divZaIzborFilmova);

        divZaIzborFilmova.appendChild(labela);

        this.kontejner.querySelector('select[name="selectZaIzborKategorije"]').onclick = (ev) => {
            if (this.kontejner.querySelector('select[name="selektFilm"]') != null) {
                let kont = this.kontejner.querySelector('select[name="selektFilm"]'); //.parentNode; ipak ne treba da brises selekt

                kont.parentNode.removeChild(kont); 
            }
            var divZaSelekt = document.createElement("div");
            let selFilmova = document.createElement("select");
            selFilmova.name = "selektFilm";
            divZaIzborFilmova.appendChild(divZaSelekt);
            divZaSelekt.appendChild(selFilmova);

            izabranaKategorija = this.kontejner.querySelector('select[name="selectZaIzborKategorije"]').value;

            fetch("https://localhost:5001/zoo/PreuzmiFilmove/" + izabranaKategorija +"/"+ this.id, {
                method: "GET",
            }).then(p => {
                p.json().then(data => {
                    data.forEach((d, i) => {
                        var opcija1 = document.createElement("option");
                        opcija1.name = "opcija";
                        opcija1.innerHTML = d.ime;
                        opcija1.value = d.id;
                        selFilmova.appendChild(opcija1);
                    })
                })
            })
        }

        var elLabela = document.createElement("label");
        elLabela.innerHTML = "Ocena";
        kontForma.appendChild(elLabela);

        var inputOcena = document.createElement("input");
        inputOcena.className = "ocena";
        kontForma.appendChild(inputOcena);

        const dugme = document.createElement("button");
        dugme.innerHTML = "Dodaj ocenu";
        kontForma.appendChild(dugme);

        dugme.onclick = (ev) => {
            var ocena = parseInt(this.kontejner.querySelector(".ocena").value);

            if ((ocena>10) || (ocena<0))
                alert ("Neispravna vrednost ocene van opsega");
            else {

            var idFilma = this.kontejner.querySelector('select[name="selektFilm"]').value;
            console.log(idFilma);
            fetch("https://localhost:5001/zoo/IzmenaPodatakaOFilmu/" + ocena + "/" + idFilma, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    ocena: ocena,
                })
            }).then(p => {
                if (p.ok) {
                    fetch("https://localhost:5001/zoo/UcitavanjeTriTopFilmaKategorije/" + izabranaKategorija + "/" + this.id, {
                        method: "GET",
                    }).then(p => {
                        p.json().then(d => {
                             
                               var divZaOcene=this.kontejner.querySelector(".divZaCrtanjeOcena");
                               if (divZaOcene!=null)
                                { 
                                   this.kontejner.removeChild(divZaOcene);
                                }
                                var divZaCrtanjeOcena = document.createElement("div");
                                divZaCrtanjeOcena.className = "divZaCrtanjeOcena";
                                this.kontejner.appendChild(divZaCrtanjeOcena);

                                var film1 =new Film(d[0].id, d[0].ime, d[0].ocena,d[0].brojOcena);
                                var film2 =new Film(d[1].id, d[1].ime, d[1].ocena,d[1].brojOcena);
                                var film3 =new Film(d[2].id, d[2].ime, d[2].ocena,d[2].brojOcena);
                                film1.crtajFilm(divZaCrtanjeOcena);
                                film2.crtajFilm(divZaCrtanjeOcena);
                                film3.crtajFilm(divZaCrtanjeOcena);

                            })
                        })
                    
                } else {
                    alert("Greška prilikom upisa.");
                }
            }).catch(p => {
                alert("Greška prilikom upisa.");
            })
            }
        }
    }
}