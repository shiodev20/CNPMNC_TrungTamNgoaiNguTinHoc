<template>
  <div class="exam_parent">
    <baselist v-if="this.$route.query.action == null"></baselist>
    <baseadd
      v-if="
        this.$route.query.action == 'create' ||
        this.$route.query.action == 'edit'
      "
    ></baseadd>
  </div>
</template>
<script>
import add from "@/components/certificate/Add_certificateV.vue";
import list from "@/components/certificate/List_cetificateV.vue";
export default {
  name: "ExamV",
  components: {
    baseadd: add,
    baselist: list,
  },
};
</script>
<style scoped></style>
