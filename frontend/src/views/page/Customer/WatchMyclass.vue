<template>
  <div class="parnet">
    <baseclass v-if="this.$route.query.action != 'detail'"></baseclass>
    <basedetail v-if="this.$route.query.action == 'detail'"></basedetail>
  </div>
</template>
<script>
import classx from "@/components/customer/Myclass/List_Watch_class.vue";
import detail_clas from "@/components/customer/Myclass/Detail_Class.vue";
export default {
  components: {
    baseclass: classx,
    basedetail: detail_clas,
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
