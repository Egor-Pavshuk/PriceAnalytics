<script setup>
import TopBar from './components/TopBar.vue'
import FilterBar from './components/FilterBar.vue'
import DailyPrices from './components/DailyPrices.vue'
import UploadFiles from './components/UploadFiles.vue'
import { ref } from 'vue'

const dailyPrices = ref([]);
const orders = ref([]);

function updatePrices(newPrices)
{
  dailyPrices.value = newPrices;
}

function updateOrders(newOrders)
{
  orders.value = newOrders;
}

</script>

<template>
  <div class="back">
    <img class="logo" src="./assets/TopPolygon.svg" />
  </div>
    <TopBar />
    <FilterBar @update-prices="updatePrices" @update-orders="updateOrders"/>
    <DailyPrices :pricesData="dailyPrices" :ordersData="orders"/>
    <UploadFiles />
</template>

<style scoped>
.back
{
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 40%;
}
.logo
{
  position: absolute;
  width: 100%;
  height: 100%;
}

</style>
