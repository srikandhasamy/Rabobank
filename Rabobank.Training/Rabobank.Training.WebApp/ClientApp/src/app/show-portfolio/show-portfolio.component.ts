import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-show-portfolio',
  templateUrl: './show-portfolio.component.html'
})
export class ShowPortfolioComponent {
  public positions: PositionVM[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<PositionVM[]>(baseUrl + 'api/portfolio').subscribe(result => {
      console.log(result);
      this.positions = result;
    }, error => console.error(error));
  }
}

export class PortfolioVM {
  Positions!: PositionVM[];
}

export class PositionVM {
  code!: string;
  name!: string;
  value!: number;
  mandates!: MandateVM[];


}

export class MandateVM {
  name!: string;
  allocation!: number;
  value!: number;
}
