export class Film {
    constructor(id, naziv, ocena, brojOcena) {
        this.id = id;
        this.naziv = naziv;
        this.ocena = ocena;
        this.brojOcena = brojOcena;
        this.miniKontejner = null;
        this.miniKontejner1 = null;
       
    }
    crtajFilm(host) {
        this.miniKontejner = document.createElement("div");
        this.miniKontejner.className = "lok";

        this.miniKontejner1 = document.createElement("div");
        this.miniKontejner1.className = "popuna";
        this.miniKontejner1.innerHTML = " "+ this.naziv +" ocena "+ this.ocena;
        
        this.miniKontejner1.style.flexGrow = this.ocena / 10;
        this.miniKontejner.appendChild(this.miniKontejner1);

        host.appendChild(this.miniKontejner);
    }
}