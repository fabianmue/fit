import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyCreateDialogComponent } from './company-create-dialog.component';

describe('CompanyCreateDialogComponent', () => {
  let component: CompanyCreateDialogComponent;
  let fixture: ComponentFixture<CompanyCreateDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompanyCreateDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CompanyCreateDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
