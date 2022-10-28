import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DisableListDetailComponent } from './disable-list-detail.component';

describe('DisableListDetailComponent', () => {
  let component: DisableListDetailComponent;
  let fixture: ComponentFixture<DisableListDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DisableListDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DisableListDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
