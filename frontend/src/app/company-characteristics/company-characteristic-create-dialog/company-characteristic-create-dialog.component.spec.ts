import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyCharacteristicCreateDialogComponent } from './company-characteristic-create-dialog.component';

describe('CompanyCharacteristicCreateDialogComponent', () => {
  let component: CompanyCharacteristicCreateDialogComponent;
  let fixture: ComponentFixture<CompanyCharacteristicCreateDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompanyCharacteristicCreateDialogComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(
      CompanyCharacteristicCreateDialogComponent
    );
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
