import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

import { getCharacteristicIcon } from '@app/shared/get-characteristic-icon';
import { HistoricCharacteristicReadDto } from '@app/api/models/historic-characteristic-read-dto';

@Component({
  selector: 'app-historic-characteristic',
  imports: [MatButtonModule, MatIconModule],
  templateUrl: './historic-characteristic.component.html',
  styleUrl: './historic-characteristic.component.scss',
})
export class HistoricCharacteristicComponent {
  @Input({ required: true })
  historicCharacteristic!: HistoricCharacteristicReadDto;
  @Output() update = new EventEmitter<void>();
  @Output() delete = new EventEmitter<void>();

  get icon(): string {
    return getCharacteristicIcon(this.historicCharacteristic);
  }

  onUpdate(): void {
    this.update.emit();
  }

  onDelete(): void {
    this.delete.emit();
  }
}
