<script setup>

const { pricesData, ordersData } = defineProps(
  {
    pricesData: Array,
    ordersData: Array
  })

  const title = ["Date", "Hour", "Price", "Offered volume", "Accepted volume", "Type", "Comments"];

  function getMatchingColor(row)
  {
    const { date, hour, price, offeredVolume, applicationType } = row;
    var resultColor = 'transparent';

    if (ordersData && Array.isArray(ordersData))
    {
      for(const order of ordersData)
      {
        if(order.date === date && order.applicationType === applicationType)
        {
          for(const rowData of order.rows || [])
          {
            for(const hourData of rowData.hours || [])
            {
              if(hourData.hour === hour && hourData.price === price)
              {
                if(hourData.volume === offeredVolume)
                {
                  resultColor = 'lightgreen';
                }
                else
                {
                  resultColor = 'yellow';
                }

                break;
              }
            }
          }
        }
      }
    }

    return resultColor;
  }

</script>

<template>
    <div class="container">
      <div class="table-wrapper">
        <h3 class="title">Prices</h3>
        <div class="table-block">
          <table class="table">
            <thead>
              <tr>
                <th v-for="(header, index) in title" :key="index">{{ header }}</th>
              </tr>
           </thead>
            <tbody>
              <tr v-for="(row, index) in pricesData" :key="index" :style="{ backgroundColor : row.color ? row.color : getMatchingColor(row) }">
                <td v-for="(value, index) in Object.values(row).slice(0, Object.values(row).length - 1)">{{ value }}</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
</template>

<style scoped>
.container
{
  display: flex;
  width: 100%;
  height: calc(100vh - 220px);
  box-sizing: border-box;
  position: fixed;
  left: 0%;
  top: 220px;
  z-index: 1000;
}

.table-wrapper
{
  width: 100%;
  display: flex;
  align-items: center;
  flex-direction: column;
  padding: 0px 5px 5px 5px;
}

.table-block{
  width: 95%;
  display: flex;
  align-items: center;
  flex-direction: column;
  overflow: auto;
}

.table{
  width: 100%;
  border-spacing: 0;
  border: 0;
  padding: 2px;
  overflow: auto;
}

.table td, .table th {
  padding: 8px;
  border: 1px solid #ccc;
  text-align: left;
}

.table th {
  position: sticky;
  top: 0;
  background-color: #fff;
  border: 2px solid #8b8989;
  border-radius: 2px 2px 0 0;
  z-index: 1;
}

.title
{
  font-size: 28px;
  font-weight: 600;
  margin: 0 0 16px 0;
}

</style>
