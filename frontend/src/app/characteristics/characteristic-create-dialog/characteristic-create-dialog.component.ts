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
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';

import { CharacteristicType } from '@app/api/models/characteristic-type';

@Component({
  imports: [
    ReactiveFormsModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatRadioModule,
    MatSelectModule,
  ],
  templateUrl: './characteristic-create-dialog.component.html',
  styleUrls: ['./characteristic-create-dialog.component.scss'],
})
export class CharacteristicCreateDialogComponent {
  characteristicFormGroup = new FormGroup({
    historic: new FormControl(true, Validators.required),
    type: new FormControl(CharacteristicType.Other, Validators.required),
    label: new FormControl('', Validators.required),
    color: new FormControl('', Validators.required),
  });

  constructor(
    private dialogRef: MatDialogRef<CharacteristicCreateDialogComponent>
  ) {}

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
