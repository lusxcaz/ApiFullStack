import { HttpClient } from '@angular/common/http';
import { Component, OnInit, inject } from '@angular/core';
import {Heroi} from '../app/models/heroi';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule, NumberValueAccessor } from '@angular/forms';
import { SuperPoder } from './models/superpoder';
import { HeroiSuperPoder } from './models/heroisuperpoderes';
import { map } from 'rxjs/operators';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ CommonModule, FormsModule ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
  
})

export class AppComponent implements OnInit{

  title = 'Consumir-Api';
  http = inject(HttpClient);
  urlApi = 'https://localhost:7112';
  _heroi$?: Observable<Heroi[]>;
  _heroiencontrar$?: Observable<Heroi>;
  _SuperPoder$?: Observable<SuperPoder[]>;

  ngOnInit(): void {
    this.CarregarHerois()
    this.CarregarPoderes()
  }
    //Variavel
    _idBuscar: number | undefined;
    _IdHeroi: number = 0;
    _nome: string = "";
    _heroi: string="";
    _data: Date = new Date;
    _altura:number = 0;
    _peso: number = 0;

   //Carregar
  CarregarHerois(){
    this._heroi$ = this.http.get<Heroi[]>(`${this.urlApi}/herois`)
  }

  CarregarHeroisPorId() {

      this._heroi$ = this.http.get<Heroi[]>(`${this.urlApi}/herois/${this._idBuscar}`)

  }

  CarregarPoderes(){
    this._SuperPoder$ = this.http.get<SuperPoder[]>(`${this.urlApi}/superpoder`)
  }

  //Salvar

  
  SalvarHeroi(){
    
    if(!this._nome)
        return alert("Preencha os campos")
    const HeroiObjSalvar: Heroi ={
      id: 0,
      nome: this._nome,
      nomeHeroi: this._heroi,
      dataNascimento: this._data,
      altura: this._altura,
      peso: this._peso
    }

    this.http.post<void>(`${this.urlApi}/herois`, HeroiObjSalvar)
              .subscribe(resultado =>{
                this.CarregarHerois()
                alert('Herói inserido com sucesso')
                this._nome = "";
                this._heroi = "";
                this._data = new Date;
                this._altura = 0;
                this._peso = 0;
              });

  }

  ObterDadosHerois(heroi: Heroi){
    this._IdHeroi = heroi.id,
    this._nome = heroi.nome,
    this._heroi = heroi.nomeHeroi,
    this._data = heroi.dataNascimento,
    this._altura = heroi.altura,
    this._peso = heroi.peso
  }

  AtualizarHeroi(){

    const HeroiObjAtualizar: Heroi ={
      id: this._IdHeroi,
      nome: this._nome,
      nomeHeroi: this._heroi,
      dataNascimento: this._data,
      altura: this._altura,
      peso: this._peso
    }

    if(!this._nome || !this._heroi)
        return alert("Prenchaa os campos")

        this.http.put<Heroi>(`${this.urlApi}/herois/${this._IdHeroi}`,HeroiObjAtualizar)
              .subscribe(resultado =>{
                this.CarregarHerois()
                alert('Herói Atualizado com sucesso')
                this._nome = "";
                this._heroi = "";
                this._data = new Date;
                this._altura = 0;
                this._peso = 0;
              })
               
  }
  
  DeletarHeroi() {

      this.http.delete<Heroi[]>(`${this.urlApi}/herois/${this._IdHeroi}`).subscribe(resultado=>{
        alert('Herói apagado com sucesso')
        this.CarregarHerois()
      });

  }




}
