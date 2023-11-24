<template>
  <div class="parnet">
    <baseclass v-if="this.$route.query.action != 'detail'"></baseclass>
    <basedetail v-if="this.$route.query.action == 'detail'"></basedetail>
  </div>
</template>
<script>
import classx from "@/components/customer/Myexam/List_Watch_Exambatch.vue";
import Detail_Exmbatch from "@/components/customer/Myexam/Detail_Exambatch.vue";
export default {
  components: {
    baseclass: classx,
    basedetail: Detail_Exmbatch,
  },
  setup() {},
};
</script>
<style scoped>
.parnet {
  position: absolute;
  inset: 0;
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>
