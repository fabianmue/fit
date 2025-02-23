import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewEncapsulation,
} from '@angular/core';
import { DecimalPipe } from '@angular/common';
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

import { CompanyCharacteristicReadDto } from '../api/models/company-characteristic-read-dto';
import { CompanyCharacteristicUpdateDto } from '../api/models/company-characteristic-update-dto';

@Component({
  selector: 'app-company-characteristic',
  imports: [
    DecimalPipe,
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
  ],
  templateUrl: './company-characteristic.component.html',
  styleUrl: './company-characteristic.component.scss',
  // encapsulation: ViewEncapsulation.None,
})
export class CompanyCharacteristicComponent implements OnInit {
  @Input({ required: true })
  companyCharacteristic!: CompanyCharacteristicReadDto;
  @Output() update = new EventEmitter<{
    companyCharacteristicUpdateDto: CompanyCharacteristicUpdateDto;
  }>();
  @Output() delete = new EventEmitter<void>();
  companyCharacteristicFormGroup = new FormGroup({
    value: new FormControl<number>(0, Validators.required),
    unit: new FormControl<string | null>(null),
  });

  constructor() {
    this.companyCharacteristicFormGroup.disable();
  }

  ngOnInit(): void {
    this.companyCharacteristicFormGroup.patchValue(this.companyCharacteristic);
  }

  onUpdate(): void {
    if (this.companyCharacteristicFormGroup.disabled) {
      this.companyCharacteristicFormGroup.enable();
      return;
    }

    this.companyCharacteristicFormGroup.disable();
    const companyCharacteristicUpdateDto: CompanyCharacteristicUpdateDto = {
      value: this.companyCharacteristicFormGroup.value.value!,
      unit: this.companyCharacteristicFormGroup.value.unit ?? null,
    };
    this.update.emit({ companyCharacteristicUpdateDto });
  }

  onDelete(): void {
    this.delete.emit();
  }
}
