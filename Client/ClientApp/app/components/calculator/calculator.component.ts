import { Component, Inject, Input, } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'calculator',
    templateUrl: './calculator.component.html',
})


export class CalculatorComponent {
    public materias: Materia[];
    public setClickedRow: Function;
    public calculateTotal: Function;
    public selectedRows: number[];
    private http: Http;
    public selectedMaterias = [];
    public price: number;

    constructor(http: Http) {
        this.http = http;
        http.get("http://localhost:55075/api/materias").subscribe(result => {
            this.materias = result.json() as Materia[];
            console.log(this.materias.toString());
        });
        this.calculateTotal = function () {
            let totalPrice = 0;
            for (let mat of this.selectedMaterias) {
                    totalPrice = totalPrice + (mat.price * mat.quantity);
                    console.log(mat);
            }
            this.price=totalPrice
        }
        this.setClickedRow = function (index) {
            this.materias[index].selected = !this.materias[index].selected;
            if (this.selectedMaterias.indexOf(this.materias[index])> -1) {
                this.selectedMaterias.splice(this.selectedMaterias.indexOf(this.materias[index]), 1);
                this.totalPrice = this.calculateTotal()
            }
            else {
                this.materias[index].quantity = 0;
                this.selectedMaterias.push(this.materias[index]);
                this.totalPrice = this.calculateTotal()
            }
        }
    }
}

interface Materia {
    id: number,
    name: string,
    price: number,
    stock: number,
    dateModified: string,
    selected: boolean
    quantity:number
}
