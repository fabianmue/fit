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

import { CompanyCreateDto } from '../api/models/company-create-dto';

@Component({
  imports: [
    ReactiveFormsModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  templateUrl: './company-create-dialog.component.html',
  styleUrls: ['./company-create-dialog.component.scss'],
})
export class CompanyCreateDialogComponent {
  companyFormGroup = new FormGroup({
    name: new FormControl('', Validators.required),
    logoUrl: new FormControl('', [
      Validators.required,
      Validators.pattern('https://.+'),
    ]),
  });

  constructor(private dialogRef: MatDialogRef<CompanyCreateDialogComponent>) {}

  onSubmit(): void {
    if (this.companyFormGroup.valid) {
      const company: CompanyCreateDto = this.companyFormGroup.value;
      this.dialogRef.close(company);
    }
  }

  onCancel(): void {
    this.dialogRef.close(null);
  }
}
