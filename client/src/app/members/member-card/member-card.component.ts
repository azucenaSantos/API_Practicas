import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { Member } from 'src/app/_modules/member';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css'],
})
export class MemberCardComponent implements OnInit {
  //Este es hijo del member-list.component
  //Tenemos que pasarle la lista de members que tiene el padre, usaremos el input
  @Input() member: Member | undefined; //evitamos error si solo ponemos Member

  constructor() {}

  ngOnInit(): void {}
}
