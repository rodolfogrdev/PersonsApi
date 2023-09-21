import { Component, OnInit } from '@angular/core';
import { Person } from 'src/app/models/person.model';
import { PersonService } from 'src/app/services/person.service';

@Component({
  selector: 'app-add-person',
  templateUrl: './add-person.component.html',
  styleUrls: ['./add-person.component.css']
})
export class AddPersonComponent implements OnInit {

  person: Person = {
    name: '',
    address: '',
    phoneNumber: '',
    emailAddress: ''
  };
  submitted = false;
  constructor(private personService: PersonService) { }

  ngOnInit(): void {
  }

  savePerson(): void {
    const data = {
      name: this.person.name,
      address: this.person.address,
      phoneNumber: this.person.phoneNumber,
      emailAddress: this.person.emailAddress
    };

    this.personService.create(data)
      .subscribe(
        response => {
          console.log(response);
          this.submitted = true;
        },
        error => {
          console.log(error);
        });
  }

  newPerson(): void {
    this.submitted = false;
    this.person = {
      name: '',
      address: '',
      phoneNumber: '',
      emailAddress: ''
    };
  }

}
