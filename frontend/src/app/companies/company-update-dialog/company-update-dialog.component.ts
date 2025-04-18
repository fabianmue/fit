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

import { CompanyUpdateDto } from '@app/api/models/company-update-dto';

@Component({
  imports: [
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
  ],
  templateUrl: './company-update-dialog.component.html',
  styleUrls: ['./company-update-dialog.component.scss'],
})
export class CompanyUpdateDialogComponent {
  companyFormGroup = new FormGroup({
    name: new FormControl('', Validators.required),
    logoUrl: new FormControl('', [
      Validators.required,
      Validators.pattern('https://.+'),
    ]),
  });

  constructor(
    private dialogRef: MatDialogRef<CompanyUpdateDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { name: string; logoUrl: string }
  ) {
    this.companyFormGroup.patchValue(data);
  }

  onSubmit(): void {
    if (this.companyFormGroup.valid) {
      const company: CompanyUpdateDto = this.companyFormGroup.value;
      this.dialogRef.close(company);
    }
  }

  onCancel(): void {
    this.dialogRef.close(null);
  }
}
