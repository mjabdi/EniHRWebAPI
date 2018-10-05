import { Component, OnInit } from '@angular/core';
import * as Chartist from 'chartist';
import {EmployeeService} from '../employees/employee.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  completness : number;

  activeCounts : number;

  constructor(private employeeService : EmployeeService ) { }

  startAnimationForLineChart(chart){
      let seq: any, delays: any, durations: any;
      seq = 0;
      delays = 80;
      durations = 500;

      chart.on('draw', function(data) {
        if(data.type === 'line' || data.type === 'area') {
          data.element.animate({
            d: {
              begin: 600,
              dur: 700,
              from: data.path.clone().scale(1, 0).translate(0, data.chartRect.height()).stringify(),
              to: data.path.clone().stringify(),
              easing: Chartist.Svg.Easing.easeOutQuint
            }
          });
        } else if(data.type === 'point') {
              seq++;
              data.element.animate({
                opacity: {
                  begin: seq * delays,
                  dur: durations,
                  from: 0,
                  to: 1,
                  easing: 'ease'
                }
              });
          }
      });

      seq = 0;
  };
  startAnimationForBarChart(chart){
      let seq2: any, delays2: any, durations2: any;

      seq2 = 0;
      delays2 = 80;
      durations2 = 1000;
      chart.on('draw', function(data) {
        if(data.type === 'bar'){
            seq2++;
            data.element.animate({
              opacity: {
                begin: seq2 * delays2,
                dur: durations2,
                from: 0,
                to: 1,
                easing: 'ease'
              }
            });
        }
      });

      seq2 = 0;
  };
  ngOnInit() {
      /* ----------==========     Daily Sales Chart initialization For Documentation    ==========---------- */

    this.employeeService.getActiveCount().subscribe( (val : number) =>
      this.activeCounts = val
    );

    this.employeeService.getCompleteness().subscribe( (val : number) =>
      this.completness = val
    );



      const dataDailySalesChart: any = {
          labels: ['Mo', 'Tu', 'We', 'Th', 'Fr'],
          series: [
              [12, 17, 7, 17, 23]
          ]
      };

     const optionsDailySalesChart: any = {
          lineSmooth: Chartist.Interpolation.cardinal({
              tension: 0
          }),
          low: 0,
          high: 50, // creative tim: we recommend you to set the high sa the biggest value + something for a better look
          chartPadding: { top: 0, right: 0, bottom: 0, left: 0},
      }

      var dailySalesChart = new Chartist.Bar('#dailySalesChart', dataDailySalesChart, optionsDailySalesChart);

      this.startAnimationForBarChart(dailySalesChart);

    // var dataDailySalesChart: any = {
    //   series: [60 ,35, 5],
    //   labels: ['Male', 'Female', 'N/A']
    // };

    // var options = {
    //   labelInterpolationFnc: function(value) {
    //     return value[0]
    //   }
    // };
    
   
    // var dailySalesChart = new Chartist.Pie('.ct-chart', dataDailySalesChart, options);





      /* ----------==========     Completed Tasks Chart initialization    ==========---------- */

      const dataCompletedTasksChart: any = {
        labels: ['Mo', 'Tu', 'We', 'Th', 'Fr'],
        series: [
            [10, 6, 17, 7, 17]
        ]
    };

     const optionsCompletedTasksChart: any = {
          lineSmooth: Chartist.Interpolation.cardinal({
              tension: 0
          }),
          low: 0,
          high: 50, // creative tim: we recommend you to set the high sa the biggest value + something for a better look
          chartPadding: { top: 0, right: 0, bottom: 0, left: 0}
      }

      var completedTasksChart = new Chartist.Bar('#completedTasksChart', dataCompletedTasksChart, optionsCompletedTasksChart);

      // start animation for the Completed Tasks Chart - Line Chart
      this.startAnimationForBarChart(completedTasksChart);





      /* ----------==========     Emails Subscription Chart initialization    ==========---------- */

      var datawebsiteViewsChart = {
        labels: ['Mo', 'Tu', 'We', 'Th', 'Fr'],
        series: [
            [7, 13, 20, 5, 12]
        ]
    };

      var optionswebsiteViewsChart = {
          axisX: {
              showGrid: false
          },
          low: 0,
          high: 30,
          chartPadding: { top: 0, right: 0, bottom: 0, left: 0}
      };
      var responsiveOptions: any[] = [
        ['screen and (max-width: 640px)', {
          seriesBarDistance: 5,
          axisX: {
            labelInterpolationFnc: function (value) {
              return value[0];
            }
          }
        }]
      ];
      var websiteViewsChart = new Chartist.Bar('#websiteViewsChart', datawebsiteViewsChart, optionswebsiteViewsChart, responsiveOptions);

      //start animation for the Emails Subscription Chart
      this.startAnimationForBarChart(websiteViewsChart);
  }

}
