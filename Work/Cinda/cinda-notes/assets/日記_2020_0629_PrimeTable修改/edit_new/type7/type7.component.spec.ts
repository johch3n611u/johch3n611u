import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Type7Component } from './type7.component';

describe('Type7Component', () => {
  let component: Type7Component;
  let fixture: ComponentFixture<Type7Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Type7Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Type7Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
