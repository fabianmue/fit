import { AsyncPipe } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Observable } from 'rxjs';

import { CharacteristicReadDto } from '@app/api/models/characteristic-read-dto';
import { AuthenticationService } from '@app/shared/authentication.service';
import { getCharacteristicIcon } from '@app/shared/get-characteristic-icon';

@Component({
  selector: 'app-characteristic',
  imports: [AsyncPipe, MatButtonModule, MatIconModule],
  templateUrl: './characteristic.component.html',
  styleUrl: './characteristic.component.scss',
})
export class CharacteristicComponent {
  @Input({ required: true }) characteristic!: CharacteristicReadDto;
  @Output() update = new EventEmitter<void>();
  @Output() delete = new EventEmitter<void>();
  authenticated$: Observable<boolean>;

  constructor(private readonly authenticationService: AuthenticationService) {
    this.authenticated$ = this.authenticationService.authenticated$;
  }

  get icon(): string {
    return getCharacteristicIcon(this.characteristic);
  }

  onUpdate(): void {
    this.update.emit();
  }

  onDelete(): void {
    this.delete.emit();
  }
}
