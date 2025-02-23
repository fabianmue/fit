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
import { MatInputModule } from '@angular/material/input';

import { CharacteristicReadDto } from '../api/models/characteristic-read-dto';
import { CharacteristicUpdateDto } from '../api/models/characteristic-update-dto';

@Component({
  imports: [
    ReactiveFormsModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  templateUrl: './characteristic-update-dialog.component.html',
  styleUrls: ['./characteristic-update-dialog.component.scss'],
})
export class CharacteristicUpdateDialogComponent {
  characteristicFormGroup = new FormGroup({
    color: new FormControl('', Validators.required),
    icon: new FormControl('', Validators.required),
    label: new FormControl('', Validators.required),
  });

  constructor(
    private dialogRef: MatDialogRef<CharacteristicUpdateDialogComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: { characteristic: CharacteristicReadDto }
  ) {
    this.characteristicFormGroup.patchValue(data.characteristic);
  }

  onSubmit(): void {
    if (this.characteristicFormGroup.valid) {
      const characteristic: CharacteristicUpdateDto =
        this.characteristicFormGroup.value;
      this.dialogRef.close(characteristic);
    }
  }

  onCancel(): void {
    this.dialogRef.close(null);
  }
}
