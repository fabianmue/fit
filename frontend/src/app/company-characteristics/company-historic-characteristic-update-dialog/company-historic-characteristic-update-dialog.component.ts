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
import { CompanyHistoricCharacteristicReadDto } from '@app/api/models/company-historic-characteristic-read-dto';

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
  templateUrl: './company-historic-characteristic-update-dialog.component.html',
  styleUrls: ['./company-historic-characteristic-update-dialog.component.scss'],
})
export class CompanyHistoricCharacteristicUpdateDialogComponent {
  companyHistoricCharacteristicFormGroup = new FormGroup({
    unit: new FormControl<string | null>(null),
    values: new FormArray<
      FormGroup<{
        value: FormControl<number | null>;
        date: FormControl<Date | null>;
      }>
    >([]),
  });

  constructor(
    private dialogRef: MatDialogRef<CompanyHistoricCharacteristicUpdateDialogComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: {
      company: CompanyReadDto;
      companyHistoricCharacteristic: CompanyHistoricCharacteristicReadDto;
    }
  ) {
    this.data.companyHistoricCharacteristic.values?.forEach((value) => {
      this.addCompanyHistoricCharacteristicValue(
        value.value!,
        new Date(value.date!)
      );
    });
    this.companyHistoricCharacteristicFormGroup.patchValue({
      unit: data.companyHistoricCharacteristic.unit,
    });
  }

  getCharacteristicIcon(characteristic: HistoricCharacteristicReadDto): string {
    return getCharacteristicIcon(characteristic);
  }

  addCompanyHistoricCharacteristicValue(
    value: number | null,
    date: Date | null
  ): void {
    (
      this.companyHistoricCharacteristicFormGroup.get('values') as FormArray
    ).push(
      new FormGroup({
        value: new FormControl<number | null>(value, Validators.required),
        date: new FormControl<Date | null>(date, Validators.required),
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
