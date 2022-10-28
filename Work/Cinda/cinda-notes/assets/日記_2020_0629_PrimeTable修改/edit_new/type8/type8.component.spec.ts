import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Type8Component } from './type8.component';

describe('Type8Component', () => {
  let component: Type8Component;
  let fixture: ComponentFixture<Type8Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Type8Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Type8Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
