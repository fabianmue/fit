import { AsyncPipe, DecimalPipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { Observable } from 'rxjs';

import { CompanyCharacteristicReadDto } from '@app/api/models/company-characteristic-read-dto';
import { AuthenticationService } from '@app/shared/authentication.service';
import { getCharacteristicIcon } from '@app/shared/get-characteristic-icon';

@Component({
  selector: 'app-company-characteristic',
  imports: [
    AsyncPipe,
    DecimalPipe,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    ReactiveFormsModule,
  ],
  templateUrl: './company-characteristic.component.html',
  styleUrl: './company-characteristic.component.scss',
})
export class CompanyCharacteristicComponent implements OnInit {
  @Input({ required: true })
  companyCharacteristic!: CompanyCharacteristicReadDto;
  @Output() update = new EventEmitter<
    Partial<{ value: number | null; unit: string | null }>
  >();
  @Output() delete = new EventEmitter<void>();
  authenticated$: Observable<boolean>;
  companyCharacteristicFormGroup = new FormGroup({
    value: new FormControl<number | null>(null, Validators.required),
    unit: new FormControl<string | null>(null),
  });

  constructor(private readonly authenticationService: AuthenticationService) {
    this.authenticated$ = this.authenticationService.authenticated$;
    this.companyCharacteristicFormGroup.disable();
  }

  ngOnInit(): void {
    this.companyCharacteristicFormGroup.patchValue(this.companyCharacteristic);
  }

  get icon(): string {
    return getCharacteristicIcon(this.companyCharacteristic);
  }

  onUpdate(): void {
    if (this.companyCharacteristicFormGroup.disabled) {
      this.companyCharacteristicFormGroup.enable();
      return;
    }

    this.companyCharacteristicFormGroup.disable();
    this.update.emit(this.companyCharacteristicFormGroup.value);
  }

  onDelete(): void {
    this.delete.emit();
  }
}
