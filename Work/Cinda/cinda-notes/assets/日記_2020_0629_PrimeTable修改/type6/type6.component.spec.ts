import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Type6Component } from './type6.component';

describe('Type6Component', () => {
  let component: Type6Component;
  let fixture: ComponentFixture<Type6Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Type6Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Type6Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
