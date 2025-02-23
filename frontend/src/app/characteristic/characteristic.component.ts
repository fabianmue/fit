import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

import { CharacteristicReadDto } from '../api/models/characteristic-read-dto';

@Component({
  selector: 'app-characteristic',
  imports: [MatButtonModule, MatIconModule],
  templateUrl: './characteristic.component.html',
  styleUrl: './characteristic.component.scss',
})
export class CharacteristicComponent {
  @Input({ required: true }) characteristic!: CharacteristicReadDto;
  @Output() update = new EventEmitter<void>();
  @Output() delete = new EventEmitter<void>();

  onUpdate(): void {
    this.update.emit();
  }

  onDelete(): void {
    this.delete.emit();
  }
}
