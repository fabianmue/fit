import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyHistoricCharacteristicUpdateDialogComponent } from './company-historic-characteristic-update-dialog.component';

describe('CompanyHistoricCharacteristicUpdateDialogComponent', () => {
  let component: CompanyHistoricCharacteristicUpdateDialogComponent;
  let fixture: ComponentFixture<CompanyHistoricCharacteristicUpdateDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompanyHistoricCharacteristicUpdateDialogComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(
      CompanyHistoricCharacteristicUpdateDialogComponent
    );
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
