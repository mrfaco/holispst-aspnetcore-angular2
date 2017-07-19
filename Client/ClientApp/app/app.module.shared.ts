import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { MateriaComponent } from './components/Materias/materias.component';
import { CreatorComponent } from './components/creator/creator.component';
import { UpdaterComponent } from './components/updater/updater.component'
import { CalculatorComponent } from "./components/calculator/calculator.component";



export const sharedConfig: NgModule = {
    bootstrap: [AppComponent],
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        MateriaComponent,
        CreatorComponent,
        UpdaterComponent,
        CalculatorComponent
    ],
    imports: [
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'calculator', pathMatch: 'full' },
            { path: 'calculator', component: CalculatorComponent },
            { path: 'materias', component: MateriaComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
};
