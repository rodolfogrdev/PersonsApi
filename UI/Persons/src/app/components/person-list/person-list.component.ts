import { Component, OnInit } from '@angular/core';
import { Person } from 'src/app/models/person.model';
import { PersonService } from 'src/app/services/person.service';

@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
  styleUrls: ['./person-list.component.css']
})
export class PersonListComponent implements OnInit {

  persons?: Person[];
  currentPerson: Person = {};
  currentIndex = -1;
  name = '';

  constructor(private personService: PersonService) { }

  ngOnInit(): void {
    this.retrievePersons();
  }

  retrievePersons(): void {
    this.personService.getAll()
      .subscribe(
        data => {
          this.persons = data;
          console.log(data);
        },
        error => {
          console.log(error);
        });
  }

  refreshList(): void {
    this.retrievePersons();
    this.currentPerson = {};
    this.currentIndex = -1;
  }

  setActivePerson(person: Person, index: number): void {
    this.currentPerson = person;
    this.currentIndex = index;
  }

  removeAllPersons(): void {
    this.personService.deleteAll()
      .subscribe(
        response => {
          console.log(response);
          this.refreshList();
        },
        error => {
          console.log(error);
        });
  }

  searchName(): void {
    this.currentPerson = {};
    this.currentIndex = -1;

    this.personService.findByName(this.name)
      .subscribe(
        data => {
          this.persons = data;
          console.log(data);
        },
        error => {
          console.log(error);
        });
  }

}
