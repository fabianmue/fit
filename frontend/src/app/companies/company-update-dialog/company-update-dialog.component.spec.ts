import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyUpdateDialogComponent } from './company-update-dialog.component';

describe('CompanyUpdateDialogComponent', () => {
  let component: CompanyUpdateDialogComponent;
  let fixture: ComponentFixture<CompanyUpdateDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompanyUpdateDialogComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CompanyUpdateDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
