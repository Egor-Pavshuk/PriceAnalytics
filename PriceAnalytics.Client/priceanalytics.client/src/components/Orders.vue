<script setup>

const { ordersData } = defineProps(
  {
    ordersData: Array
  })

</script>

<template>
    <div class="container">
      <div class="table-wrapper">
        <h3 class="title">Orders</h3>
        <div class="table-block">
          <div class="table-container" v-for="(order, index) in ordersData" :key="index">
          <h4 class="order-header">
            <span>Date: {{ order.date }}</span>
            <span>|</span>
            <span>Type: {{ order.applicationType }}</span>
          </h4>
          <table class="table">
            <tbody>
              <tr v-for="(row, rIndex) in order.rows" :key="rIndex">
                <td v-for="(hourData, hIndex) in row.hours" :key="hIndex">
                  <div class="order-cell-container">
                    <div class="order-cell" style="font-weight: bold; background-color: #dfe1e1;">{{ hourData.hour }}</div>
                    <div class="order-cell">{{ hourData.volume }}</div>
                    <div class="order-cell">{{ hourData.price }}</div>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
          </div>
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
  width: 100%;
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

.table td {
  padding: 0px;
  border: 1px solid #ccc;
}

.title
{
  font-size: 28px;
  font-weight: 600;
  margin: 0 0 16px 0;
}

.order-header
{
  display: flex;
  gap: 12px;
  align-items: center;
  font-weight: 600;
  background: #dfe1e1;
  padding: 8px 12px;
  border-radius: 8px;
  margin-bottom: 8px;
  margin-top: 16px;
  font-size: 1rem;
  color: #333;
}

.order-cell
{
  width: 100%;
  text-align: center;
}

.order-cell-container
{
  width: 100%;
  height: 70px;
}

.table-container
{
  width: 95%;
  align-items: center;
}
</style>
