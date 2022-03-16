export class Film {
    constructor(id, naziv, ocena, brojOcena) {
        this.id = id;
        this.naziv = naziv;
        this.ocena = ocena;
        this.brojOcena = brojOcena;
        this.miniKontejner = null;
        this.miniKontejner1 = null;
        this.miniKontejner0= null;
       
    }
    crtajFilm(host) {
        
        this.miniKontejner0=document.createElement("div");
        this.miniKontejner0.className="glavniKontejnerZaPrikazFilma"

        var labelaZaOcenu = document.createElement("label");
        labelaZaOcenu.className= "labelaZaOcenu";
        labelaZaOcenu.innerHTML=this.ocena;
        this.miniKontejner0.appendChild(labelaZaOcenu);

            this.miniKontejner = document.createElement("div");
            this.miniKontejner.className = "glavniKontejnerZaPrikazFilma1";

        this.miniKontejner0.appendChild(this.miniKontejner);

                this.miniKontejner1 = document.createElement("div");
                this.miniKontejner1.className = "popuna";
        
                this.miniKontejner1.style.flexGrow = this.ocena / 10;
            this.miniKontejner.appendChild(this.miniKontejner1);
        
        var labelaZaNaziv = document.createElement("label");
        labelaZaNaziv.className= "labelaZaNaziv";
        labelaZaNaziv.innerHTML=this.naziv;
        this.miniKontejner0.appendChild(labelaZaNaziv)


        host.appendChild(this.miniKontejner0);
    }
}
