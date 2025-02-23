import { Component, Inject } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialogRef,
  MatDialogModule,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

import { CompanyReadDto } from '../api/models/company-read-dto';
import { CompanyCharacteristicCreateDto } from '../api/models/company-characteristic-create-dto';
import { CharacteristicReadDto } from '../api/models/characteristic-read-dto';

@Component({
  imports: [
    ReactiveFormsModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatSelectModule,
  ],
  templateUrl: './company-characteristic-create-dialog.component.html',
  styleUrls: ['./company-characteristic-create-dialog.component.scss'],
})
export class CompanyCharacteristicCreateDialogComponent {
  companyCharacteristicFormGroup = new FormGroup({
    characteristicId: new FormControl('', Validators.required),
    value: new FormControl<number | null>(null, Validators.required),
    unit: new FormControl(''),
  });

  constructor(
    private dialogRef: MatDialogRef<CompanyCharacteristicCreateDialogComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: {
      company: CompanyReadDto;
      characteristicOptions: Array<CharacteristicReadDto>;
    }
  ) {}

  onSubmit(): void {
    if (this.companyCharacteristicFormGroup.valid) {
      const companyCharacteristic: CompanyCharacteristicCreateDto = {
        characteristicId:
          this.companyCharacteristicFormGroup.value.characteristicId!,
        companyId: this.data.company.id,
        value: this.companyCharacteristicFormGroup.value.value!,
        unit: this.companyCharacteristicFormGroup.value.unit,
      };
      this.dialogRef.close(companyCharacteristic);
    }
  }

  onCancel(): void {
    this.dialogRef.close(null);
  }
}
