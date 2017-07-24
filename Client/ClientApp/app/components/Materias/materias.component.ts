import { Component, Inject, Input,} from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'materias',
    templateUrl: './materias.component.html',
})


export class MateriaComponent {
    public materias: Materia[];
    public setClickedRow: Function;
    public selectedRow: -2;
    public selectedMateria: Materia;
    public isSelected = false;
    private http: Http;

    constructor(http: Http) {
        this.http = http;
        http.get("http://holispst.azurewebsites.net/api/materias").subscribe(result => {
            this.materias = result.json() as Materia[];
            console.log(this.materias.toString());
        });
        this.setClickedRow = function (index) {
            if (this.selectedRow == index) {
                this.selectedRow = -1;
                this.isSelected = false;
                this.selectedMateria = null;
            }
            else {
                this.selectedRow = index;
                this.isSelected = true;
                this.selectedMateria = this.materias[this.selectedRow];
            }
        }
    }
    onUpdated(event) {
        console.log("event received");
        this.http.get("http://holispst.azurewebsites.net/api/materias").subscribe(result => {
            this.materias = result.json() as Materia[];
            console.log(this.materias.toString());
        });
        this.selectedMateria = null;
        this.selectedRow = null;
        this.isSelected = false;

    }
    
}

interface Materia {
    id: number,
    name: string,
    price: number,
    stock: number,
    dateModified: string
}
