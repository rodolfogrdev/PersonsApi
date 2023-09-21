import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Person } from 'src/app/models/person.model';
import { PersonService } from 'src/app/services/person.service';

@Component({
  selector: 'app-person-details',
  templateUrl: './person-details.component.html',
  styleUrls: ['./person-details.component.css']
})
export class PersonDetailsComponent implements OnInit {

  currentPerson: Person = {
    name: '',
    address: '',
    phoneNumber: '',
    emailAddress: ''
  };
  message = '';

  constructor(private personService: PersonService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.message = '';
    this.getPerson(this.route.snapshot.params['id']);
  }

  getPerson(id: string): void {
    this.personService.get(id)
      .subscribe(
        data => {
          this.currentPerson = data;
          console.log(data);
        },
        error => {
          console.log(error);
        });
  }  

  updatePerson(): void {
    this.message = '';

    this.personService.update(this.currentPerson.id, this.currentPerson)
      .subscribe(
        response => {
          console.log(response);
          this.message = response.message ? response.message : 'This person was updated successfully!';
        },
        error => {
          console.log(error);
        });
  }

  deletePerson(): void {
    this.personService.delete(this.currentPerson.id)
      .subscribe(
        response => {
          console.log(response);
          this.router.navigate(['/persons']);
        },
        error => {
          console.log(error);
        });
  }

}
