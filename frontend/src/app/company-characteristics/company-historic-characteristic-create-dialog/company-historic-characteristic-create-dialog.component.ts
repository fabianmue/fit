import { Component, Inject } from '@angular/core';
import {
  FormArray,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import {
  MatDialogRef,
  MatDialogModule,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

import { getCharacteristicIcon } from '@app/shared/get-characteristic-icon';
import { CompanyReadDto } from '@app/api/models/company-read-dto';
import { HistoricCharacteristicReadDto } from '@app/api/models/historic-characteristic-read-dto';

@Component({
  providers: [provideNativeDateAdapter()],
  imports: [
    ReactiveFormsModule,
    MatButtonModule,
    MatDatepickerModule,
    MatDialogModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatSelectModule,
  ],
  templateUrl: './company-historic-characteristic-create-dialog.component.html',
  styleUrls: ['./company-historic-characteristic-create-dialog.component.scss'],
})
export class CompanyHistoricCharacteristicCreateDialogComponent {
  companyHistoricCharacteristicFormGroup = new FormGroup({
    historicCharacteristicId: new FormControl('', Validators.required),
    unit: new FormControl<string | null>(null),
    values: new FormArray([
      new FormGroup({
        value: new FormControl<number | null>(null, Validators.required),
        date: new FormControl<Date | null>(null, Validators.required),
      }),
    ]),
  });

  constructor(
    private dialogRef: MatDialogRef<CompanyHistoricCharacteristicCreateDialogComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: {
      company: CompanyReadDto;
      historicCharacteristicOptions: Array<HistoricCharacteristicReadDto>;
    }
  ) {}

  getCharacteristicIcon(characteristic: HistoricCharacteristicReadDto): string {
    return getCharacteristicIcon(characteristic);
  }

  addCompanyHistoricCharacteristicValue(): void {
    (
      this.companyHistoricCharacteristicFormGroup.get('values') as FormArray
    ).push(
      new FormGroup({
        value: new FormControl<number | null>(null, Validators.required),
        date: new FormControl<Date | null>(null, Validators.required),
      })
    );
  }

  removeCompanyHistoricCharacteristicValue(index: number): void {
    (
      this.companyHistoricCharacteristicFormGroup.get('values') as FormArray
    ).removeAt(index);
  }

  onSubmit(): void {
    this.dialogRef.close(this.companyHistoricCharacteristicFormGroup.value);
  }

  onCancel(): void {
    this.dialogRef.close(null);
  }
}
