import { AsyncPipe } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Observable } from 'rxjs';

import { HistoricCharacteristicReadDto } from '@app/api/models/historic-characteristic-read-dto';
import { AuthenticationService } from '@app/shared/authentication.service';
import { getCharacteristicIcon } from '@app/shared/get-characteristic-icon';

@Component({
  selector: 'app-historic-characteristic',
  imports: [AsyncPipe, MatButtonModule, MatIconModule],
  templateUrl: './historic-characteristic.component.html',
  styleUrl: './historic-characteristic.component.scss',
})
export class HistoricCharacteristicComponent {
  @Input({ required: true })
  historicCharacteristic!: HistoricCharacteristicReadDto;
  @Output() update = new EventEmitter<void>();
  @Output() delete = new EventEmitter<void>();
  authenticated$: Observable<boolean>;

  constructor(private readonly authenticationService: AuthenticationService) {
    this.authenticated$ = this.authenticationService.authenticated$;
  }

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
