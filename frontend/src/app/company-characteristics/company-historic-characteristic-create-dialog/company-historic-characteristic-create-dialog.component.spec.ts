import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyHistoricCharacteristicCreateDialogComponent } from './company-historic-characteristic-create-dialog.component';

describe('CompanyHistoricCharacteristicCreateDialogComponent', () => {
  let component: CompanyHistoricCharacteristicCreateDialogComponent;
  let fixture: ComponentFixture<CompanyHistoricCharacteristicCreateDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompanyHistoricCharacteristicCreateDialogComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(
      CompanyHistoricCharacteristicCreateDialogComponent
    );
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
