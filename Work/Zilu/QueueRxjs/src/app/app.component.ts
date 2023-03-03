import { QueueService } from './../queue/queue.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'QueueRxjs';
  request = '';
  response = [] as any[];

  constructor(private queueService: QueueService) {
    queueService.outgoing$.subscribe((res: any) => {
      this.response.push(res);
    });
  }

  add() {
    this.queueService.add(this.request);
  }
}
