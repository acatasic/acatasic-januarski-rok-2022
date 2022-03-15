import { Kategorija } from "./Kategorija.js";
import { ProdKuca } from "./prodkuce.js"

var glavniDiv=document.createElement("div")
glavniDiv.className="glavniDiv";
document.body.appendChild(glavniDiv);

fetch("https://localhost:5001/zoo/PreuzmiPKuce").then(p => {
    p.json().then(data => {
        data.forEach(pk => {
            const vrt1 = new ProdKuca(pk.id,pk.Naziv);
            vrt1.crtajPK(glavniDiv);
            });
        });
});

