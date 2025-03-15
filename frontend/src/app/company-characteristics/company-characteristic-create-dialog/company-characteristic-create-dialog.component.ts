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

import { getCharacteristicIcon } from '@app/shared/get-characteristic-icon';
import { CharacteristicReadDto } from '@app/api/models/characteristic-read-dto';
import { CompanyReadDto } from '@app/api/models/company-read-dto';

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
    unit: new FormControl<string | null>(null),
    value: new FormControl<number | null>(null, Validators.required),
  });

  constructor(
    private dialogRef: MatDialogRef<CompanyCharacteristicCreateDialogComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: {
      company: CompanyReadDto;
      characteristicOptions: Array<CharacteristicReadDto>;
    }
  ) {}

  getCharacteristicIcon(characteristic: CharacteristicReadDto): string {
    return getCharacteristicIcon(characteristic);
  }

  onSubmit(): void {
    this.dialogRef.close(this.companyCharacteristicFormGroup.value);
  }

  onCancel(): void {
    this.dialogRef.close(null);
  }
}
