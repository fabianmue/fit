import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { CharacteristicCreateDto } from '../api/models/characteristic-create-dto';

@Component({
  imports: [
    ReactiveFormsModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  templateUrl: './characteristic-create-dialog.component.html',
  styleUrls: ['./characteristic-create-dialog.component.scss'],
})
export class CharacteristicCreateDialogComponent {
  characteristicFormGroup = new FormGroup({
    color: new FormControl('', Validators.required),
    icon: new FormControl('', Validators.required),
    label: new FormControl('', Validators.required),
  });

  constructor(
    private dialogRef: MatDialogRef<CharacteristicCreateDialogComponent>
  ) {}

  onSubmit(): void {
    if (this.characteristicFormGroup.valid) {
      const characteristic: CharacteristicCreateDto =
        this.characteristicFormGroup.value;
      this.dialogRef.close(characteristic);
    }
  }

  onCancel(): void {
    this.dialogRef.close(null);
  }
}
