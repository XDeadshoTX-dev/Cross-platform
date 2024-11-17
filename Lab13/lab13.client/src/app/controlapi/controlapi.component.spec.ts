import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ControlapiComponent } from './controlapi.component';

describe('ControlapiComponent', () => {
  let component: ControlapiComponent;
  let fixture: ComponentFixture<ControlapiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ControlapiComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ControlapiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
