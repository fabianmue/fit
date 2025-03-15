import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyHistoricCharacteristicComponent } from './company-historic-characteristic.component';

describe('CompanyHistoricCharacteristicComponent', () => {
  let component: CompanyHistoricCharacteristicComponent;
  let fixture: ComponentFixture<CompanyHistoricCharacteristicComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompanyHistoricCharacteristicComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CompanyHistoricCharacteristicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
