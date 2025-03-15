import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HistoricCharacteristicComponent } from './historic-characteristic.component';

describe('HistoricCharacteristicComponent', () => {
  let component: HistoricCharacteristicComponent;
  let fixture: ComponentFixture<HistoricCharacteristicComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HistoricCharacteristicComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(HistoricCharacteristicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
