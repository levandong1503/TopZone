import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-type-card',
  standalone: true,
  imports: [],
  templateUrl: './type-card.component.html',
  styleUrls: ['./type-card.component.scss']
})
export class TypeCardComponent {
  @Input() imageSrc: string = '';
  @Input() titleType: string = '';
}
