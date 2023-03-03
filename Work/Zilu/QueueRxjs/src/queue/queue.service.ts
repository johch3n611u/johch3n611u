import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject, timer } from 'rxjs';
import { debounce, filter, switchMap, takeUntil, withLatestFrom } from 'rxjs/operators';
import { Observable, from } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class QueueService {

  // 緩衝不立即發出的資料
  private pending$ = new BehaviorSubject<any>([]);
  // 等待發出的資料
  public outgoing$ = new BehaviorSubject<any>([]);
  // 當符合條件時發出資料
  private isReadyToEmit$ = new Subject<boolean>();
  private ngUnsubscribe = new Subject();

  BUFFER_SIZE = 5;
  DEBOUNCE_TIME = 5000;

  constructor() {
    this.getPendingEvent().pipe(
      // 發出符合給定條件的值
      filter(items => items && items.length > 0),
      // 根據一個選擇器函數，捨棄掉在兩次輸出之間小於指定時間的發出值
      debounce(items =>
        timer(
          items.length >= Number(this.BUFFER_SIZE) ? 0 : (Number(this.DEBOUNCE_TIME))
        ),
      ),
      // 發出值，直到提供的 observable 發出值，它便完成
      takeUntil(this.ngUnsubscribe)
    ).subscribe(pendingItems => {
      // 分類：超過 BUFFER_SIZE 筆數則等待下一次發送
      const outgoingList = pendingItems.slice(0, Number(this.BUFFER_SIZE)) || [];
      const restOfPendingList = pendingItems.slice(outgoingList.length, pendingItems.length) || [];
      if (outgoingList.length > 0) {
        this.outgoing$.next(outgoingList);
        this.isReadyToEmit$.next(true);
      }
      this.pending$.next(restOfPendingList);
    });

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
    );
  }

  ngOnDestroy() {
    console.log('ngOnDestroy');
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
