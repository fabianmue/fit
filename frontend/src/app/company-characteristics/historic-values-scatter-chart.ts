import { Chart } from 'chart.js/auto';

import { CompanyHistoricCharacteristicReadDto } from '@app/api/models/company-historic-characteristic-read-dto';
import { HistoricValueReadDto } from '@app/api/models/historic-value-read-dto';

export function configureCompanyHistoricCharacteristicScatterChart(
  element: HTMLCanvasElement,
  companyHistoricCharacteristic: CompanyHistoricCharacteristicReadDto
): Chart<'scatter'> {
  const chartRanges = calculateChartRanges(
    companyHistoricCharacteristic.values
  );
  return new Chart(element, {
    type: 'scatter',
    data: {
      datasets: [
        {
          label: companyHistoricCharacteristic.label!,
          data:
            companyHistoricCharacteristic.values?.map((value) => ({
              x: new Date(value.date!).getTime(),
              y: value.value!,
            })) ?? [],
        },
      ],
    },

    options: {
      plugins: {
        legend: { display: false },
        tooltip: {
          displayColors: false,
          callbacks: {
            label: (toolTipItem) => {
              return `${new Date(
                toolTipItem.parsed.x
              ).toLocaleDateString()} - ${toolTipItem.parsed.y} ${
                companyHistoricCharacteristic.unit
              }`;
            },
          },
        },
      },
      scales: {
        x: {
          title: { display: false },
          ticks: {
            count: 5,
            callback: (tickValue, index, ticks) =>
              index === 0 || index === ticks.length - 1
                ? new Date(tickValue).toLocaleDateString()
                : '',
          },
          offset: true,
          min: chartRanges.xMin,
          max: chartRanges.xMax,
        },
        y: {
          title: {
            display: !!companyHistoricCharacteristic.unit,
            text: companyHistoricCharacteristic.unit ?? '',
          },
          ticks: { count: 4 },
          offset: true,
        },
      },
    },
  });
}

function calculateChartRanges(
  values: Array<HistoricValueReadDto> | null | undefined
): { xMin: number; xMax: number } {
  if (!values?.length || values.length < 2) {
    return { xMin: 0, xMax: 1 };
  }

  const xValues = values!.map((value) => new Date(value.date!).getTime());
  return { xMin: Math.min(...xValues), xMax: Math.max(...xValues) };
}
