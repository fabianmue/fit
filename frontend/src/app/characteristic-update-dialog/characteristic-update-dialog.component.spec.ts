import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CharacteristicUpdateDialogComponent } from './characteristic-update-dialog.component';

describe('CharacteristicUpdateDialogComponent', () => {
  let component: CharacteristicUpdateDialogComponent;
  let fixture: ComponentFixture<CharacteristicUpdateDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CharacteristicUpdateDialogComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CharacteristicUpdateDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
