import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject, timer } from 'rxjs';
import { debounce, filter, switchMap, takeUntil, withLatestFrom } from 'rxjs/operators';
import { Observable, from } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class QueueService {

  private pending$ = new BehaviorSubject<any>([]);
  public outgoing$ = new BehaviorSubject<any>([]);
  private isReadyToEmit$ = new Subject<boolean>();
  private ngUnsubscribe = new Subject();

  BUFFER_SIZE = 5;
  DEBOUNCE_TIME = 5000;

  constructor() {
    this.getPendingEvent().pipe(
      filter(items => items && items.length > 0),
      debounce(items =>
        timer(
          items.length >= Number(this.BUFFER_SIZE) ? 0 : (Number(this.DEBOUNCE_TIME))
        ),
      ),
      takeUntil(this.ngUnsubscribe)
    ).subscribe(pendingItems => {
      const outgoingList = pendingItems.slice(0, Number(this.BUFFER_SIZE)) || [];
      const restOfPendingList = pendingItems.slice(outgoingList.length, pendingItems.length) || [];
      if (outgoingList.length > 0) {
        this.outgoing$.next(outgoingList);
        this.isReadyToEmit$.next(true);
      }
      this.pending$.next(restOfPendingList);
    })

    this.isReadyToEmit$.pipe(
      withLatestFrom(this.getOutgoingEvent()),
      switchMap(([isReadyToEmit, outgoingEvents]) => {
        return this.request(isReadyToEmit, outgoingEvents)
      }),
      takeUntil(this.ngUnsubscribe)
    ).subscribe(
      res => {
        this.clearOutgoingEvents();
      }
    )
  }

  ngOnDestroy() {
    this.ngUnsubscribe.complete();
  }

  add(event: any) {
    this.pending$.next([...this.pending$.getValue(), ...[event]]);
  }

  getPendingEvent() {
    return this.pending$.asObservable();
  }

  getOutgoingEvent() {
    return this.outgoing$.asObservable();
  }

  clearOutgoingEvents() {
    this.outgoing$.next([]);
  }

  request(isReadyToEmit: any, outgoingEvents: any): Observable<any> {
    return from(outgoingEvents);
  }
}
