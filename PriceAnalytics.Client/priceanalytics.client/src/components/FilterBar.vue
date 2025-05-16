<script setup>
import { ref } from 'vue';

const selectedMode = ref('singleMode')
const selectedDate = ref(formatDate(new Date()))
const selectedStartDate = ref(formatDate(new Date()))
const selectedEndDate = ref(formatDate(new Date()))

const emitMethods = defineEmits(['update-prices', 'update-orders']);

singleDateChanged();

function formatDate(date)
{
  return date.toISOString().split('T')[0]
}

async function singleDateChanged()
{
  const pricesResponse = await fetch("https://localhost:7260/api/filters/prices/by-single-date?date=" + selectedDate.value,
    {
      method : 'GET'
    }
  );

    const ordersResponse = await fetch("https://localhost:7260/api/filters/orders/by-single-date?date=" + selectedDate.value,
    {
      method : 'GET'
    }
  );

  var pricesData = await pricesResponse.json();
  var ordersData = await ordersResponse.json();

  emitMethods('update-orders', ordersData);
  emitMethods('update-prices', pricesData);
}

async function rangeDateChanged()
{
  const pricesResponse = await fetch("https://localhost:7260/api/filters/prices/by-date-range?startDate=" + selectedStartDate.value + "&endDate=" + selectedEndDate.value,
    {
      method : 'GET'
    }
  );

  const ordersResponse = await fetch("https://localhost:7260/api/filters/orders/by-date-range?startDate=" + selectedStartDate.value + "&endDate=" + selectedEndDate.value,
    {
      method : 'GET'
    }
  );

  var pricesData = await pricesResponse.json();
  var ordersData = await ordersResponse.json();

  emitMethods('update-orders', ordersData);
  emitMethods('update-prices', pricesData);
}

async function startDateChanged()
{
  if(selectedStartDate.value > selectedEndDate.value)
  {
    selectedEndDate.value = selectedStartDate.value;
  }

  await rangeDateChanged();
}

async function endDateChanged()
{
  if(selectedEndDate.value < selectedStartDate.value)
  {
    selectedStartDate.value = selectedEndDate.value;
  }

  await rangeDateChanged();
}

</script>

<template>
    <div class="filter-bar">
      <div class="filter-block">
      <h3 class="filter-title">Choose date mode</h3>
      <div class="radio-group">
        <label class="radio-option">
          <input type="radio" value="singleMode" v-model="selectedMode" />
          Single date mode
        </label>
        <label class="radio-option">
          <input type="radio" value="rangeMode" v-model="selectedMode" />
          Range date mode
        </label>
      </div>
      </div>
      <div class="single-date-block" v-show="selectedMode === 'singleMode'">
      <h3 class="filter-title">Choose date</h3>
      <div class="date-group">
        <input type="date" v-model="selectedDate" @change="singleDateChanged()" />
      </div>
      </div>
      <div class="range-date-block" v-show="selectedMode === 'rangeMode'">
      <h3 class="filter-title">Choose date range</h3>
      <div class="range-date-group">
        <input type="date" v-model="selectedStartDate" @change="startDateChanged" />
        <input type="date" v-model="selectedEndDate" @change="endDateChanged" />
      </div>
      </div>
    </div>
</template>

<style scoped>
.filter-bar
{
  display: flex;
  align-items: center;
  justify-content: flex-start;
  gap: 60px;
  padding: 16px;
  width: 100%;
  height: 70px;
  box-sizing: border-box;
  position: fixed;
  left: 30%;
  top: 80px;
  z-index: 1000;
}

.filter-block
{
  display: flex;
  align-items: center;
  flex-direction: column;
  gap: 10px;
}

.filter-title
{
  font-size: 16px;
  font-weight: 600;
  margin: 0;
}

.radio-group
{
  display: flex;
  gap: 16px;
}

.radio-option
{
  display: flex;
  align-items: center;
  gap: 4px;
  cursor: pointer;
}

input[type="radio"]
{
  cursor: pointer;
}

.single-date-block
{
  display: flex;
  align-items: center;
  flex-direction: column;
  gap: 10px;
}

input[type="date"]
{
  cursor: pointer;
  border-radius: 5px;
  border: 1px solid #a2c7ea;
  padding: 5px;
}

.range-date-block
{
  display: flex;
  align-items: center;
  flex-direction: column;
  gap: 10px;
}

.range-date-group
{
  display: flex;
  align-items: center;
  gap: 20px;
}

</style>
