import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CharacteristicsDashboardComponent } from './characteristics-dashboard.component';

describe('CharacteristicsDashboardComponent', () => {
  let component: CharacteristicsDashboardComponent;
  let fixture: ComponentFixture<CharacteristicsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CharacteristicsDashboardComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CharacteristicsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
