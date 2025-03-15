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
import { MatSelectModule } from '@angular/material/select';

import { CharacteristicType } from '@app/api/models/characteristic-type';

@Component({
  imports: [
    ReactiveFormsModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
  ],
  templateUrl: './characteristic-update-dialog.component.html',
  styleUrls: ['./characteristic-update-dialog.component.scss'],
})
export class CharacteristicUpdateDialogComponent {
  characteristicFormGroup = new FormGroup({
    type: new FormControl(CharacteristicType.Other, Validators.required),
    label: new FormControl('', Validators.required),
    color: new FormControl('', Validators.required),
  });

  constructor(
    private dialogRef: MatDialogRef<CharacteristicUpdateDialogComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: {
      type: CharacteristicType;
      label: string;
      color: string;
    }
  ) {
    this.characteristicFormGroup.patchValue(data);
  }

  get typeOptions(): Array<string> {
    return Object.keys(CharacteristicType);
  }

  onSubmit(): void {
    this.dialogRef.close(this.characteristicFormGroup.value);
  }

  onCancel(): void {
    this.dialogRef.close(null);
  }
}
