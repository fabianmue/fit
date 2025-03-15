import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyCharacteristicComponent } from './company-characteristic.component';

describe('CompanyCharacteristicComponent', () => {
  let component: CompanyCharacteristicComponent;
  let fixture: ComponentFixture<CompanyCharacteristicComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompanyCharacteristicComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CompanyCharacteristicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
