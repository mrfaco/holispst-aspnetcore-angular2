import { Component, Inject, Input, Output, ElementRef, EventEmitter } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from "rxjs/Rx";
import 'rxjs/Rx';

@Component({
    selector: 'updater',
    templateUrl: './updater.component.html'
})

export class UpdaterComponent {
    private http: Http;
    public updated = false;
    public _materiaInput: any;
    @Output() onUpdated = new EventEmitter<boolean>();
    public BaseUrl: string;

    @Input()
    set materiaInput(inp: any) {
        this._materiaInput = Object.assign({},inp);
        this.updated = false;
    }
    get materiaInput() {
        return this._materiaInput;
    }

    constructor(http: Http, @Inject('ORIGIN_URL') BaseUrl: string) {
        this.http = http;
        this.BaseUrl = BaseUrl;
    }

    updateSelected() {

        let mat =
            {
                'Id': this.materiaInput.Id,
                'Name': this.materiaInput.name,
                'Price': this.materiaInput.price,
                'Stock': this.materiaInput.stock,
                'DateModified': Date.now()
            };
        console.log(mat);
        this.postToApi(<Materia>mat)
            .subscribe(
            data => {
                console.log(data);
                this.updated = true;
                this.onUpdated.emit(true);
                return true
            },
            error => {
                console.error("error");
                return Observable.throw(error);
            }
            );
    }

    deleteSelected() {
        this.deleteFromApi(this.materiaInput.id)
            .subscribe(
            data => {
                console.log(data);
                this.updated = true;
                this.onUpdated.emit(true);
                return true;
            },
            error => {
                console.error("error");
                return Observable.throw(error);
            }
            );
    }


    update(formvalues) {
        console.log(formvalues);
        let mat =
            {
                'Id': formvalues.id!=null ? formvalues.id:this.materiaInput.id,
                'Name': formvalues.name != null ? formvalues.name : this.materiaInput.name,
                'Price': formvalues.price != null ? formvalues.price : this.materiaInput.price,
                'Stock': formvalues.stock != null ? formvalues.stock : this.materiaInput.stock,
                'DateModified': Date.now()
            };
        console.log(mat);
        this.postToApi(<Materia>mat)
            .subscribe(
            data => {
                console.log(data);
                this.updated = true;
                this.onUpdated.emit(true);
                return true
            },
            error => {
                console.error("error");
                return Observable.throw(error);
            }
            );
    }

    deleteFromApi(id: number) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this.http.delete(this.BaseUrl+"/Materias/Delete/"+id,options)
            .map((res: Response) => res.text());
    }

    postToApi(mat: Materia) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let body = JSON.stringify([mat]);
        console.log(body);
        return this.http.put(this.BaseUrl + "/Materias/Update", body, options)
            .map((res: Response) => res.text());
    }
}

interface Materia {
    Id: number,
    Name: string,
    Price: number,
    Stock: number,
    DateModified: number
}