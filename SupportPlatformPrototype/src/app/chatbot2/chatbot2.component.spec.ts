/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { Chatbot2Component } from './chatbot2.component';

describe('Chatbot2Component', () => {
  let component: Chatbot2Component;
  let fixture: ComponentFixture<Chatbot2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Chatbot2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Chatbot2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
