import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CharacteristicCreateDialogComponent } from './characteristic-create-dialog.component';

describe('CharacteristicCreateDialogComponent', () => {
  let component: CharacteristicCreateDialogComponent;
  let fixture: ComponentFixture<CharacteristicCreateDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CharacteristicCreateDialogComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CharacteristicCreateDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
